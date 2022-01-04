import { Injectable } from '@angular/core';
import axios, { AxiosRequestConfig } from 'axios';
import * as querystring from 'querystring';
import clientInfo from './clientInfo';
import store from './store';
import CenteralRequest, { ApiResponse } from './centeralRequest';
const axiosConfig: any = {
  baseURL: clientInfo.BaseUrl,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded',
    // 'withCredentials': true,
    'Access-Control-Allow-Methods': "DELETE, POST, GET, PUT, OPTIONS",
    'Access-Control-Allow-Headers': "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With",
    'Access-Control-Allow-Origin': '*',
    'Authorization': ""
  }
};
@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  setBearerToken(access_token: any) {
    var config = {
      ...axiosConfig,
    }
    config.headers["Authorization"] = `Bearer ${access_token}`
    return config;
  }
  async signInAsync({
    username = "",
    password = ""
  }) {
    const requestData = {
      client_id: clientInfo.WebClientForUser.ClientId,
      client_secret: clientInfo.WebClientForUser.ClientSecret,
      grant_type: clientInfo.GrantType.ResourceOwnerPasswordCredential,
      username: username,
      password: password
    };
    return CenteralRequest.request(async () => {
      const result = await axios.post('/connect/token', querystring.stringify(requestData), axiosConfig);
      store.set("token", result.data);
      return result;
    });
  }
  async getUserInfoAsync() {
    const currentToken = store.get("token").access_token;
    const config = this.setBearerToken(currentToken);
    return CenteralRequest.request(async () => {
      var result = await axios.get('/api/user/getuser', config);
      store.set("userInfo", result.data.data);
      return result;
    });
  }
  async getUserAsync(id: any) {
    const currentToken = store.get("token").access_token;
    const config = this.setBearerToken(currentToken);
    return CenteralRequest.request(async () => {
      var result = await axios.get(`/api/user/getuser/${id}`, config);
      store.set("userInfo", result.data.data);
      return result;
    });
  }
  async connectTokenAsync() {
    const token = store.get("webClientToken");

    if (token) {
      var now = new Date();
      var exp = new Date(token.expires_date);
      if (exp.getTime() > now.getTime()) {
        return ApiResponse.success(token);
      }
    }
    const requestData = {
      client_id: clientInfo.WebClient.ClientId,
      client_secret: clientInfo.WebClient.ClientSecret,
      grant_type: clientInfo.GrantType.ClientCredential
    };
    return CenteralRequest.request(async () => {
      const result = await axios.post('/connect/token', querystring.stringify(requestData), axiosConfig);
      var d = new Date();
      d = new Date(d.getTime() + result.data.expires_in);
      result.data.expires_date = d;
      store.set("webClientToken", result.data);
      return result;
    });
  }
  async refreshTokenAsync() {
    const currentToken = store.get("token").refresh_token;
    const requestData = {
      client_id: clientInfo.WebClientForUser.ClientId,
      client_secret: clientInfo.WebClientForUser.ClientSecret,
      grant_type: clientInfo.GrantType.ClientCredential,
      refresh_token: currentToken
    };
    return CenteralRequest.request(async () => {
      const result = await axios.post('/connect/token', querystring.stringify(requestData), axiosConfig);
      store.set("token", result.data);
      return result;
    });
  }
  async getRoles() {
    const currentToken = store.get("webClientToken").access_token;
    const config = this.setBearerToken(currentToken);
    config.headers["Content-Type"] = "application/json";
    return CenteralRequest.request(async () => {
      var result = await axios.get('/api/role', config);
      return result;
    });
  }
  async revokeRefreshTokenAsync() {
    console.log(store.get("token"));
    const currentToken = store.get("token").refresh_token;
    const requestData = {
      client_id: clientInfo.WebClientForUser.ClientId,
      client_secret: clientInfo.WebClientForUser.ClientSecret,
      refresh_token: currentToken,
      token_typ_hint: clientInfo.GrantType.RefreshTokenCredential
    };
    return CenteralRequest.request(async () => {
      const result = await axios.post('/connect/token', querystring.stringify(requestData), axiosConfig);
      store.set("token", result.data);
      return result;
    });
  } async signUpAsync(model: any) {
    const currentToken = store.get("webClientToken").access_token;
    const config = this.setBearerToken(currentToken);
    config.headers["Content-Type"] = "application/json";
    return CenteralRequest.request(async () => {
      var result = await axios.post('/api/user/signup', model, config);
      return result;
    });
  }
  async updateUserAsync(model: any) {
    const currentToken = store.get("webClientToken").access_token;
    const config = this.setBearerToken(currentToken);
    config.headers["Content-Type"] = "application/json";
    return CenteralRequest.request(async () => {
      var result = await axios.put('/api/user/update', model, config);
      return result;
    });
  }
  async getUsers() {
    const currentToken = store.get("webClientToken").access_token;
    const config = this.setBearerToken(currentToken);
    config.headers["Content-Type"] = "application/json";
    return CenteralRequest.request(async () => {
      var result = await axios.get('/api/user/getusers', config);
      return result;
    });
  }
  async signOutAsync() {
    // await this.revokeRefreshTokenAsync();
    store.remove('token');
    store.remove('userInfo');
  }
}
export default IdentityService;

import axios from "axios";
import identityServerRequest from "./identity.service";
import CenteralRequest from "./centeralRequest";
import store from "./store.js";
import clientInfo from "./clientInfo";
const requester = new identityServerRequest();
const axiosApiInstance = axios.create();

axiosApiInstance.interceptors.request.use(
  async (config) => {
    const token = store.get("token");
    config.baseURL = `http://${clientInfo.BaseUrl}/services`;
    config.headers = {
      "Authorization": `Bearer ${token.access_token}`,
      "Accept": "application/json",
      //'withCredentials': true,
      "Access-Control-Allow-Methods": "DELETE, POST, GET, PUT, OPTIONS",
      "Access-Control-Allow-Headers":
        "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With",
      "Access-Control-Allow-Origin": "*",
      "Content-Type": "application/json",
    };
    return config;
  },
  (error) => {
    return CenteralRequest.errorResponse(error);
  }
);

axiosApiInstance.interceptors.response.use(
  (response) => {
    return CenteralRequest.successResponse(response);
  },
  async function (error) {
    const originalRequest = error.config;
    if (error.response.request.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const token = await requester.refreshTokenAsync();
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + token.data.access_token;
      return axiosApiInstance(originalRequest);
    }
    return CenteralRequest.errorResponse(error);
  }
);

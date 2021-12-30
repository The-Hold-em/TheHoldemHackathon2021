import { Injectable } from '@angular/core';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class NodeService {
  apiUrl: string = 'http://localhost:5000';
  constructor() { }

  async getElectionsResult() {
    let API_URL = `${this.apiUrl}/node/voteResults`;
    return await axios.post(API_URL);
  }
}

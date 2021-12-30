import { Injectable } from '@angular/core';
import rop_axios from '../identity/rop_axios'

@Injectable({
  providedIn: 'root'
})
export class CandidateService {

  constructor() { }

  async getCandidate() {
    return await rop_axios.get("/api/candidate");
  }
}

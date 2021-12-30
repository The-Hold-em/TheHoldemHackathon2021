import { Component, OnInit } from '@angular/core';
import { PollingstationService } from 'src/app/services/pollingstation/pollingstation.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  isStart: boolean = false;
  isResult: boolean = false;
  statusText: string = "";
  constructor(private pollingstationService: PollingstationService) { }

  ngOnInit(): void {
    this.GetServerStatus();
  }
  async GetServerStatus() {
    this.isStart = await this.pollingstationService.getServerStatus();
    this.statusText = this.isStart ? "Seçim devam ediyor" : "Seçim Başlatılmadı";
  }

  async startElection() {
    await this.pollingstationService.startElection();
    this.GetServerStatus();
  }
  async stopElection() {
    await this.pollingstationService.stopElection();
    this.GetServerStatus();
  }
  getResult() {

  }

}

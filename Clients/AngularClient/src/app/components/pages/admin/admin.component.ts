import { Component, OnInit } from '@angular/core';
import { CandidateService } from 'src/app/services/candidate/candidate.service';
import { NodeService } from 'src/app/services/node/node.service';
import { PollingstationService } from 'src/app/services/pollingstation/pollingstation.service';
var candidateService = new CandidateService();

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  isStart: boolean = false;
  isResult: boolean = false;
  isResultBtn: boolean = false;
  statusText: string = "";
  results: any;
  constructor(private pollingstationService: PollingstationService, private nodeService: NodeService) { }

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
    this.isResultBtn = true;
  }
  async getResult() {
    var res = await this.nodeService.getElectionsResult().then(r => r.data.VoteResults);
    var candidates = await candidateService.getCandidate().then(r => r.data);
    this.results = res.map((x: { id: any; count: any }) => {
      var found = candidates.find((c: { id: any; }) => c.id == x.id);
      return {
        ...found,
        count: x.count
      }
    });
    this.isResult = true;
  }

}

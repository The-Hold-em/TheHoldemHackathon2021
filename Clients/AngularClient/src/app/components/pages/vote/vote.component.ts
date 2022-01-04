import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cryptography } from 'src/app/models/cryptography';
import { Vote } from 'src/app/models/vote.model';
import { CandidateService } from 'src/app/services/candidate/candidate.service';
import { PollingstationService } from 'src/app/services/pollingstation/pollingstation.service';

declare let $: any;
@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.scss']
})
export class VoteComponent implements OnInit {

  candidates = [{
    id: "",
    name: ""
  }]

  radioinput: any;
  publicKey: string = "";
  privateKey: string = "";
  candidateId: any;
  constructor(private pollingStationService: PollingstationService, private router: Router) { }

  ngOnInit(): void {
    this.getCandidate();
  }

  setCandidateId(candinateId: any) {
    this.candidateId = candinateId;
  }
  async sendVote() {
    this.generateKeyPair();
    var vote = await new Vote(this.publicKey, this.privateKey, this.candidateId);
    console.log(vote);

    await this.pollingStationService.sendVote(
      {
        publicKey: vote.publicKey,
        candinateId: vote.candinateId,
        signature: vote.signature
      }
    ).subscribe(() => {
      this.router.navigate(["voted"])
    });

  }

  generateKeyPair() {
    var cryptography = new Cryptography();

    this.publicKey = cryptography.publicKey;
    console.log("publicKey:" + this.publicKey);
    this.privateKey = cryptography.privateKey;
    console.log("privateKey:" + this.privateKey);
  }

  async getCandidate() {
    var candidateService = new CandidateService();
    var result = await candidateService.getCandidate();
    this.candidates = result.data;
    console.log(this.candidates);

  }
  ngAfterViewInit() {
    $(document).ready(() => {
      setTimeout(() => {
        $('input[type="radio"]').on("change", function (e: any) {
          var candidate = $(e.target).parents(".candidate")[0];
          $(".candidate").removeClass("active");
          $(candidate).addClass("active");
        });
        $(".circle").on("click", function (e: any) {
          var radio = $(e.target).find('input[type="radio"]');
          console.log(radio);
          radio.checked = true;
          $(radio).trigger("change");
        });
      }, 100);

    });

  }
}

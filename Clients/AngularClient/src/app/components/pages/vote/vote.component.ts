import { Component, OnInit } from '@angular/core';
import { Cryptography } from 'src/app/models/cryptography';
import { Vote } from 'src/app/models/vote.model';
import { PollingstationService } from 'src/app/services/pollingstation/pollingstation.service';
declare let $: any;
@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.scss']
})
export class VoteComponent implements OnInit {

  candidates = [{
    id: "d57f351504f645b381bd9b620c134808",
    name: "Recep"
  }, {
    id: "1a70dbd3230c43c8b654c72be17cd370",
    name: "Ömer"
  }, {
    id: "eadd0b78a6d94cf88dac1012d1566a3d",
    name: "Hikmet"
  }, {
    id: "2f28503a0e104adf89ae02ec676870df",
    name: "İrfan"
  }, {
    id: "4c366ae228bf4e2f8264831885ea096d",
    name: "Elizabeth Olsen"
  }
  ]

  radioinput: any;
  publicKey: string = "";
  privateKey: string = "";
  candidateId: any;
  constructor(private pollingStationService: PollingstationService) { }

  ngOnInit(): void {
  }

  setCandidateId(candinateId: any) {
    this.candidateId = candinateId;
  }
  async sendVote() {
    this.generateKeyPair();
    var vote = await new Vote(this.publicKey, this.privateKey, this.candidateId);

    await this.pollingStationService.sendVote(
      {
        publicKey: vote.publicKey,
        candinateId: vote.candinateId,
        signature: vote.signature
      }
    ).subscribe();
  }

  generateKeyPair() {
    var cryptography = new Cryptography();

    this.publicKey = cryptography.publicKey;
    console.log("publicKey:" + this.publicKey);
    this.privateKey = cryptography.privateKey;
    console.log("privateKey:" + this.privateKey);
  }

  ngAfterViewInit() {
    $(document).ready(() => {
      $('input[type="radio"]').on("change", function (e: any) {
        var candidate = $(e.target).parents(".candidate")[0];
        $(".candidate").removeClass("active");
        $(candidate).addClass("active");
      });
      $(".circle").on("click", function (e: any) {
        var radio = $(e.target).find('input[type="radio"]');
        console.log();

        radio.checked = true;
        $(radio).trigger("change");
      });
    });

  }
}

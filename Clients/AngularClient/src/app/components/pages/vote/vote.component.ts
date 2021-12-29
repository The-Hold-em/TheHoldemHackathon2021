import { Component, OnInit } from '@angular/core';
import { Vote } from 'src/app/models/vote.model';
import { CryptographyService } from 'src/app/services/cryptography/cryptography.service';
import { PollingstationService } from 'src/app/services/pollingstation/pollingstation.service';
declare let $: any;
@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.scss']
})
export class VoteComponent implements OnInit {

  radioinput: any;
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


  publicKey: string = "";
  privateKey: string = "";
  // vote: Vote = new Vote();
  constructor(private pollingStationService: PollingstationService, private cryptographyService: CryptographyService) { }

  ngOnInit(): void {
  }

  test() {
    this.pollingStationService.sendVote({
      "publicKey": "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
      "candinateId": "1.Aday",
      "signature": "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409"
    }).subscribe();
  }

  sendVote(candinateId: any) {
    this.generateKeyPair();
    var vote = new Vote(this.publicKey, this.privateKey, candinateId);
    console.log(vote);
    this.pollingStationService.sendVote(vote).subscribe();
  }

  generateKeyPair() {
    this.publicKey = this.cryptographyService.publicKey;
    this.privateKey = this.cryptographyService.privateKey;
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

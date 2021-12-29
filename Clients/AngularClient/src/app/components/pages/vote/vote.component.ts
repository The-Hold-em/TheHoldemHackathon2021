import { Component, OnInit } from '@angular/core';
declare let $: any;
@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.scss']
})
export class VoteComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
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

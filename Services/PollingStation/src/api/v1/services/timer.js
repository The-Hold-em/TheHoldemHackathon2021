const Timer = require("setinterval");
const { SendVoteList } = require("./pollingstation");
var State = require("../helpers/state");

class ElectionTimer {
  constructor(state = 1, period = 1000) {
    this.state = state;
    this.period = period;
    this.timer = new Timer(async () => {
      SendVoteList();
    }, this.period);
  }
  // start election
  startElection() {
    this.timer.setInterval();
  }

  stopElection() {
    clearInterval(this.timer);
  }
}

exports.ElectionTimer = ElectionTimer;

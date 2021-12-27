class Vote {
  constructor(candinateId, voterPublicKey) {
    this.candinateId = candinateId;
    this.voterPublicKey = voterPublicKey;
    this.voteList = [];
  }

  setVoteList(vote) {
    this.voteList.push(vote);
  }
  getVoteList() {
    return this.voteList;
  }
}

module.exports.Vote = Vote;

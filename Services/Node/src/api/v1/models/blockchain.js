const { Block } = require("./block");
const { Vote } = require("./vote");
const { VoteList } = require("./votelist");
const State = require("../helpers/state");

class Blockchain {
  constructor() {
    this.chain = [this.createGenesisBlock()];
    this.difficulty = 4;
    this.voteList = new VoteList();
  }

  createGenesisBlock() {
    return new Block([], "0");
  }

  getGenesisBlock() {
    return this.chain[0];
  }

  getLatestBlock() {
    return this.chain[this.chain.length - 1];
  }

  getBlock(id) {
    return this.chain.find((block) => block.index == id);
  }

  addVoteList(voteList) {
    this.voteList = new VoteList(
      voteList.pollingStationPublicKey,
      voteList.signature,
      voteList.votes
    );
  }

  mineVoteList() {
    console.log(this.chain.length);
    let block = new Block(
      this.chain.length,
      this.voteList,
      this.voteList.calculateHash(),
      this.getLatestBlock().hash
    );
    block.mineBlock(this.difficulty);

    console.log("Block succesfully mined");
    this.chain.push(block);
  }

  getVoteResults() {
    var counts;
    return counts;
  }

  isChainVaild() {
    for (let i = 1; i < this.chain.length; i++) {
      const currentBlock = this.chain[i];
      const previousBlock = this.chain[i - 1];

      if (!currentBlock.hasValidVoteList(this.voteList)) {
        return false;
      }

      if (currentBlock.hash !== currentBlock.calculateHash()) {
        return false;
      }
      console.log();
      if (currentBlock.previousHash !== previousBlock.hash) {
        return false;
      }
    }
    return true;
  }
}

module.exports.Blockchain = Blockchain;

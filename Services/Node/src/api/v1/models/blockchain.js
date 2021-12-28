const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");

const { Block } = require("./block");
const { VoteList } = require("./votelist");

class Blockchain {
  constructor() {
    this.chain = [this.createGenesisBlock()];
    this.difficulty = 4;
    this.voteList = new VoteList();
  }

  createGenesisBlock() {
    return new Block([], "0");
  }

  getLatestBlock() {
    return this.chain[this.chain.length - 1];
  }

  addVoteList(voteList) {
    this.voteList = new VoteList(
      voteList.pollingStationPublicKey,
      voteList.signature,
      voteList.voteList
    );
  }

  mineVoteList() {
    let block = new Block(
      this.voteList.calculateHash(),
      this.getLatestBlock().hash
    );
    block.mineBlock(this.difficulty);

    console.log("Block succesfully mined");
    this.chain.push(block);
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

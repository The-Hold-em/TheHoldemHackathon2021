const SHA256 = require("crypto-js/sha256.js");

class Block {
  constructor(votelistHash, previousHash = "") {
    this.timestamp = Date.now();
    this.votelistHash = votelistHash;
    this.previousHash = previousHash;
    this.hash = this.calculateHash();
    this.nonce = 0;
  }
  calculateHash() {
    return SHA256(
      this.index +
        this.previousHash +
        this.timestamp +
        this.votelistHash +
        this.nonce
    ).toString();
  }

  mineBlock(difficulty) {
    while (
      this.hash.substring(0, difficulty) !== Array(difficulty + 1).join("0")
    ) {
      this.nonce++;
      this.hash = this.calculateHash();
    }
    console.log("Block mined: " + this.hash);
  }

  hasValidVoteList(votelist) {
    if (!votelist.isValid()) {
      return false;
    }
    return true;
  }
}
module.exports.Block = Block;

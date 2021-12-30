const SHA256 = require("crypto-js/sha256");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");

sign_vote = (privateKey, candinateId) => {
  const myKey = ec.keyFromPrivate(privateKey);
  const hashVote = SHA256(candinateId).toString();
  const sig = myKey.sign(hashVote, "base64");
  return sig.toDER("hex");
};

console.log(
  sign_vote(
    "97bb59d307e58399c6e1cc10a6d236e0f41e2a7466c80ed83682f669516e8af6",
    "1.Aday"
  )
);

const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");
var State = require("../helpers/state");

module.exports.sign_data = (data) => {
  const signingKey = ec.keyFromPrivate(process.env.POLLING_STATION_PRIVATE_KEY);

  if (signingKey.getPublic("hex") !== process.env.POLLING_STATION_PUBLIC_KEY) {
    throw new Error("You cannot sign this vote list!");
  }

  const hashData = SHA256(data).toString();
  const sig = signingKey.sign(hashData, "base64");
  return sig.toDER("hex");
};

module.exports.sign_vote_list = () => {
  const signingKey = ec.keyFromPrivate(process.env.POLLING_STATION_PRIVATE_KEY);

  if (signingKey.getPublic("hex") !== process.env.POLLING_STATION_PUBLIC_KEY) {
    throw new Error("You cannot sign this vote list!");
  }
  let hashItem = "";
  for (var item of State.voteListHash) {
    hashItem += item;
  }
  const hashData = SHA256(hashItem).toString();
  const sig = signingKey.sign(hashData, "base64");
  return sig.toDER("hex");
};

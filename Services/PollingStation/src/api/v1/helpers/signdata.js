const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");

module.exports.sign_data = (data) => {
  const signingKey = ec.keyFromPrivate(process.env.POLLING_STATION_PRIVATE_KEY);

  if (signingKey.getPublic("hex") !== process.env.POLLING_STATION_PUBLIC_KEY) {
    throw new Error("You cannot sign this vote list!");
  }

  const hashTx = SHA256(data).toString();
  const sig = signingKey.sign(hashTx, "base64");
  return sig.toDER("hex");
};

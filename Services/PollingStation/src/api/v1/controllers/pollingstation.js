const SHA256 = require("crypto-js/sha256.js");
const ValidateSignature = require("../helpers/validatesignature");
var State = require("../helpers/state");

exports.recevie_vote = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;
  if (ValidateSignature.validate_signature(publicKey, candinateId, signature)) {
    State.voteList.push({
      publicKey: req.body.publicKey,
      candinateId: req.body.candinateId,
      signature: req.body.signature,
    });
    State.voteListHash.push(
      SHA256(publicKey + candinateId + signature).toString()
    );
    return res.status(200).json({
      Message: "This vote is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};

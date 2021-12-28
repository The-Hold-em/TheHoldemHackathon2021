const SHA256 = require("crypto-js/sha256.js");
var State = require("../helpers/state");
const { Vote } = require("../models/vote");

exports.recevie_vote = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;
  var vote = new Vote(publicKey, candinateId, signature);
  if (vote.isValid()) {
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

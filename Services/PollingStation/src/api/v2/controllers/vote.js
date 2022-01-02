const { Vote } = require("../models/vote");

const recevie_vote = (req, res) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;
  var vote = new Vote(publicKey, candinateId, signature);
  if (vote.isValid()) {
    //mongo db migration oluştur ve mongodbye kayıt et lokal olarak
    return res.status(200).json({
      Message: "This vote is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};

module.exports = [recevie_vote];

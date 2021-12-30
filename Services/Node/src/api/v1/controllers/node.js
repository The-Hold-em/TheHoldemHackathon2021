const { VoteList } = require("../models/votelist");
const State = require("../helpers/state");

exports.receive_vote_list = (req, res, next) => {
  const pollingStationPublicKey = req.body.pollingStationPublicKey;
  const signature = req.body.signature;
  const votes = req.body.votes;

  var voteListModel = new VoteList(pollingStationPublicKey, signature, votes);
  if (voteListModel.isValid()) {
    State.Blockchain.addVoteList({
      pollingStationPublicKey:
        "046423d73500c09b602c299eb0586a2a6581fa4151211388d32077e3dd6420b43c55a205a5b23126fd39ddbed48991c2dee975358d79bbfbf75a5374dad41f217f",
      signature:
        "304502206c11316029b9d45b3f95011798d43e01e7d534c6e65d8c5610baa757c96a746102210081ccaf39d983d2060cbff18aa32ffc398b0f8cc55754b23af59775307169aa73",
      votes: [
        {
          publicKey:
            "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
          candinateId: "1.Aday",
          signature:
            "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409",
        },
        {
          publicKey:
            "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
          candinateId: "1.Aday",
          signature:
            "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409",
        },
        {
          publicKey:
            "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
          candinateId: "1.Aday",
          signature:
            "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409",
        },
        {
          publicKey:
            "04bcb77784b5db325a9774ddcc446dbcf82730b823bfd7d2253dac08f1e83cec23f43a0402fbace05705c56642ba3e9150b92550859f62d76ea0b1927d035fded1",
          candinateId: "1.Aday",
          signature:
            "304402203eec4043c31871bd1f3190d50784e5fe26b9ea4a563a965c795025f3be4dd00c022076a71d395dcda8ba5bf4b75ccf1f1f2a02d2d869b8e91935aeb55cc0fad01409",
        },
      ],
    });
    State.Blockchain.mineVoteList();
    return res.status(200).json({
      Message: "This vote list is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote list validation is failed",
    });
  }
};

exports.receive_block = (req, res, next) => {
  return res.status(200).json({
    Message: "receive_block succesfully worked",
  });
};

exports.get_block_chain = (req, res, next) => {
  return res.status(200).json({
    Blockchain: State.Blockchain.chain,
    Message: "get_block_chain succesfully worked",
  });
};

exports.get_last_block = (req, res, next) => {
  return res.status(200).json({
    Block: State.Blockchain.getLatestBlock(),
    Message: "get_last_block succesfully worked",
  });
};

exports.get_genesis_block = (req, res, next) => {
  return res.status(200).json({
    Block: State.Blockchain.getGenesisBlock(),
    Message: "get_genesis_block succesfully worked",
  });
};

exports.get_block = (req, res, next) => {
  const id = req.params.id;
  return res.status(200).json({
    Block: State.Blockchain.getBlock(id),
    Message: "get_block succesfully worked",
  });
};

exports.vote_results = (req, res, next) => {
  return res.status(200).json({
    VoteResults: State.Blockchain.getVoteResults(),
    Message: "vote_results succesfully worked",
  });
};

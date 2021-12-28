const axios = require("axios");
const SignVoteList = require("../helpers/signdata");
const State = require("../helpers/state");

exports.SendVoteList = () => {
  const data = State.votes;
  const sign = SignVoteList.sign_vote_list();
  const publicKey = process.env.POLLING_STATION_PUBLIC_KEY;

  axios
    .post("http://localhost:3000/pollingstation/sendVoteListToNode", {
      pollingStationPublicKey: publicKey,
      signature: sign,
      votes: data,
    })
    .then((res) => {
      console.log(`statusCode: ${res.status}`);
      console.log(res);
    })
    .catch((error) => {
      console.error(error);
    });
};

require("dotenv").config();
const express = require("express");
const app = express();
const morgan = require("morgan");
const bodyParser = require("body-parser");

const pollingStationRoutes = require("./api/v1/routes/pollingstation");
const sendVoteList = require("./api/v1/services/pollingstation");
const Cors = require("./api/v1/middlewares/cors");
const InternalServerError = require("./api/v1/middlewares/500");
const NotFound = require("./api/v1/middlewares/404");

app.use(morgan("dev"));
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

app.use(Cors.cors);

// Routes which should handle requests
//---
app.use("/pollingstation", pollingStationRoutes);

//---
const Timer = require("setinterval");

const t = new Timer(async () => {
  sendVoteList.SendVoteList();
}, 10000);

// start timer
t.setInterval();

app.use(InternalServerError.internal_server_err);
app.use(NotFound.not_found);

module.exports = app;

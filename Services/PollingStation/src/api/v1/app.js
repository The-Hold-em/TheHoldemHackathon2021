require("dotenv").config();
const express = require("express");
const app = express();
const morgan = require("morgan");
const bodyParser = require("body-parser");

const pollingStationRoutes = require("./routes/pollingstation");
const sendVoteList = require("./services/pollingstation");
const Cors = require("./middlewares/cors");
const InternalServerError = require("./middlewares/500");
const NotFound = require("./middlewares/404");

app.use(morgan("dev"));
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(Cors.cors);

// Routes which should handle requests
//---
app.use("/pollingstation", pollingStationRoutes);

//---

app.use(InternalServerError.internal_server_err);
app.use(NotFound.not_found);

module.exports = app;

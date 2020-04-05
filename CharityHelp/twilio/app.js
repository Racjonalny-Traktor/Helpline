const express = require('express');
const app = express();
app.disable('etag');

const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({extended: false}));
app.use(bodyParser.json());

require('dotenv').config()

app.use(express.static('public'));

const twilio = require('./routes/twilio');
app.use('/twilio', twilio);

module.exports = app;
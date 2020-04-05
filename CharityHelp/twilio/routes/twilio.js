const express = require('express');
const router = express.Router();

const twilioController = require('../controllers/twilio');
router.post("/voice", twilioController.voice);
router.post("/transcribe", twilioController.transcribe);

module.exports = router;
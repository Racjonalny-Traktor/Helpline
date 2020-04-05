const VoiceResponse = require('twilio').twiml.VoiceResponse;
const axios = require('axios').default;

const doesNumberExist = async (number) => {
  const response = await axios.get(`${process.env.BACKEND_URL}/User/${number}`);
  return response.data.exists;
}

const transcribeAddress = async (address) => {
  params = {
    address,
    key: process.env.GOOGLE_MAPS_KEY
  }
  const response = await axios.get('https://maps.googleapis.com/maps/api/geocode/json', {params});
  console.log(response.data.results[0].geometry);
  return response.data.results[0].geometry.location.lat, response.data.results[0].geometry.location.lng;
}

const createNewUser = async (number, address, lat, lng) => {
  data = {
    phoneNumber: number,
    address,
    latitude: lat,
    longitude: lng
  }
  await axios.post(`${process.env.BACKEND_URL}/User`, data);
}

const callNumber = async (number) => {
  await axios.get(`${process.env.BACKEND_URL}/Call/${number}`);
}

const voice = async (req, res) => {
  const user = await doesNumberExist(req.body.From);

  const twiml = new VoiceResponse();

  if (user) {
    twiml.play(`${process.env.TWILIO_SERVER_URL}/audio/nth_time.wav`);
    await callNumber(req.body.From);
    twiml.hangup()
  } else {
    twiml.play(`${process.env.TWILIO_SERVER_URL}/audio/first_time.wav`);
    twiml.gather({
      action: '/twilio/transcribe',
      method: 'POST',
      input: 'speech',
      language: 'pl-PL'
    })
    twiml.hangup()
  }

  res.type('text/xml');
  res.send(twiml.toString());
}

const transcribe = async (req, res) => {
  var lat, lng;
  lat, lng = await transcribeAddress(req.body.SpeechResult);
  await createNewUser(req.body.From, req.body.SpeechResult, lat, lng);
  await callNumber(req.body.From);
  res.sendStatus(200);
}

module.exports = {
  voice,
  transcribe
};
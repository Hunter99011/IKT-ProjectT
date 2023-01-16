const cors = require('cors');
const express = require('express');
const app = express();
 
app.use(cors())
 
app.get('/products/:id', function (req, res, next) {
  res.json({msg: 'This is CORS-enabled for all origins!'})
})
 
app.listen(80, function () {
  console.log('CORS-enabled web server listening on port 80')
})

fetch('http://localhost:3000/CarListDBSpec?Name=Ford',{method:'GET'})
    .then(response =>response.json())
    .then(data =>console.log(data));
fetch('http://localhost:3000/OneCarDelete?ID=2', {method: 'DELETE'})
  .then(response => response.json())
  .then(data => console.log(data));

fetch('http://localhost:3000/CarPutDB', {
  method: 'PUT',
  body: JSON.stringify({
    "ID": "5",
    "Make": "Teszt",
    "Model": "Teszt",
    "Year": "2023",
    "Color": "Pirosss",
    "Vin": "WBAWL73557P888258"
  }),
  headers: {
    'Content-Type': 'application/json'
  }
})
.then(response => response.json())
.then(data => console.log(data));
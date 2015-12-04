var express = require('express');
var app = express();
app.use(express.static(__dirname + '/static'));

app.get('*', function (req, res) {

  	res.sendFile('views/Master.html', { root: __dirname+'/' });

});


app.listen('3000');
console.log('Server started on: localhost:3000');
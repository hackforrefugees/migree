var express = require('express');
var app = express();
app.use(express.static(__dirname + '/static'));
//app.use(express.static(__dirname + '/views'));


app.get('/views/:page', function (req, res) {
  res.sendFile('/views/'+req.params.page, { root: __dirname+'/' })
});

app.get('/views/:route/:page', function (req, res) {
  res.sendFile('/views/'+req.params.route+'/'+req.params.page, { root: __dirname+'/' })
});

app.get('*', function (req, res) {

  	res.sendFile('views/_shared/Master.html', { root: __dirname+'/' });

});

app.listen('3000');
console.log('Server started on: localhost:3000');
var express = require('express');
var app = express();

app.use( require('express-force-domain')('http://migree.se') );

app.use(express.static(__dirname));


app.get('views/:page', function (req, res) {
  res.sendFile('/views/'+req.params.page, { root: __dirname+'/' })
});

app.get('views/:route/:page', function (req, res) {
  res.sendFile('/views/'+req.params.route+'/'+req.params.page, { root: __dirname+'/' })
});

app.get('*', function (req, res) {

  	res.sendFile('index.html', { root: __dirname+'/' });

});

var port = process.env.PORT || 3000
app.listen(port);
console.log('Server started on: localhost:' + port);

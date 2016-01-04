var express = require('express'),
    path = require('path');

var app = express();
app.use(express.static(path.join(__dirname, '/dist')));

app.get('*', function (req, res) {
	res.sendFile('index.html', { root: path.join(__dirname, '/dist')});
});

var port = process.env.PORT || 3000
app.listen(port);
console.log('Server started on: localhost:' + port);

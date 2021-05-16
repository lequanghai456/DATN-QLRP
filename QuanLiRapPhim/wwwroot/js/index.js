var express=require('express');
var app = express();

app.use('/',function(req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});

var server = require('http').Server(app);

const io = require('socket.io')(server, {
  cors: {
    origin: '*',
  }
});

//Whenever someone connects this gets executed
io.on('connection', function (client) {
    console.log(client.id +' connected');

    //Whenever someone disconnects this piece of code executed
    client.on('join', function (data) {
        console.log(data);
    });

    client.on('chonghe', function (data) {
        console.log(client.id +' dang chon ghe');

        client.emit('dachonghe', {
            id: data,
            isemit: false
        });
        client.broadcast.emit('dachonghe', {
            id: data,
            isemit: true
        });
    });

    client.on('huychon', function (data) {
        console.log(client.id+'thoi gian dat da het');
        client.broadcast.emit('huydachon',data);
    })

    client.on('disconnect', function () {
        console.log('A user disconnected');
    });
});

server.listen(3000, function () {
    console.log('listening on *:3000');
});
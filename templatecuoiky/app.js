// Based on the tutorial:
// http://tutorialzine.com/2012/08/nodejs-drawing-game/

//Restrictions on HEROKU:
// Doesn't support installing dependencies with npm with node 0.8
// Doesn't support websocekts.
// Including libraries
// var knox = require('knox');
/*
 var client = knox.createClient({
	key:'AKIAIDZAMIX2REQZACVA',
	secret: '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'
  , bucket: 'bucket222'
});
 */
var stringafile ='';
var stringaip ='';
var stanza ='';
/*
var object = { foo: "bar" };
var string = JSON.stringify(object);
var req = client.put('/test/obj.json', {
    'Content-Length': string.length
  , 'Content-Type': 'application/json'
});
req.on('response', function(res){
  if (200 == res.statusCode) {
    console.log('saved to %s', req.url);
  }
});
req.end(string);

*/
/////////////////////////////////////////////
// var app = require('express')();         //
// var http = require('http').Server(app); //
// var io = require('socket.io')(http);    //
/////////////////////////////////////////////
var path = require("path");
var express = require("express");
var app = express();

var fs = require('fs')
var https = require('https')
https.createServer({
  key: fs.readFileSync('host.key'),
  cert: fs.readFileSync('host.cert')
}, app)
.listen(8443, function () {
  console.log('Example app listening on port 3000! Go to https://localhost:3000/')
})
var http = require('http').Server(app);
var io = require('socket.io')(http);// for serving files
app.use(express.static(path.join(__dirname, '')));
app.get('/', function(req, res) {
   res.sendFile(__dirname + '');
});
// This will make all the files in the current folder
// accessible from the web
// var fileServer = new nstatic.Server('./');
	
// This is the port for our web server.
// you will need to go to http://localhost:3000 to see it
http.listen(3000, function() {
   console.log('listening on *:6868');
   });

// If the URL of the socket server is opened in a browser
// function handler (request, response) {
// 	request.addListener('end', function () {
//         fileServer.serve(request, response);
//     }).resume();
// }

// Delete this row if you want to see debug messages
io.set('log level', 1);

// Heroku doesn't support websockets so...
// Detect if heroku via config vars
// https://devcenter.heroku.com/articles/config-vars
// heroku config:add HEROKU=true --app node-drawing-game
if (process.env.HEROKU === 'true') {
    io.configure(function () {
        io.set("transports", ["xhr-polling"]);
        io.set("polling duration", 20);
    });
}


// Listen for incoming connections from clients
io.sockets.on('connection', function (socket) {
socket.join('public');   
console.log('connesso');

/*	   				  
									  
client.get('filelog.txt').on('response', function(res){
var miadata =	new Date();											  
stringaip = socket.handshake.address.address  + ' ' + miadata.getDate() +'/' + miadata.getMonth() + '/' + miadata.getYear() 
+ ' ' + miadata.getHours() +':' + miadata.getMinutes() +':' + miadata.getSeconds();
 // console.log(stringaip);
  // console.log(res.headers);
  res.setEncoding('utf8');
  res.on('data', function(chunk){
//    console.log(chunk);
stringafile = stringafile + chunk;
//     console.log(stringafile);
  }); 
}).end();									  

var buffer = new Buffer(stringafile + stringaip + '\n');
var headers = {
  'Content-Type': 'text/plain'
};
client.putBuffer(buffer, '/filelog.txt', headers, function(err, res){
  // Logic
});
*/

socket.on('disconnect', function () {
								  
console.log('disconnesso');								  /*
var roster = io.sockets.clients(data.room);
var listautenti = '';
roster.forEach(function(client) {
listautenti =	listautenti +  client.nickname + '<br />';
}); 
listautenti = 'A USER HAS DISCONNECTED - LIST USERS IN THIS ROOM: ' +  listautenti;

socket.broadcast.to(data.room).emit('listautentiser', {
							'listautenti' : listautenti		
									});
*/
  });


socket.on('suonacamp', function (data) {
socket.broadcast.to(data.room).emit('suonacampser', data);
});

 socket.on('setuproom', function (data) { 
 // var room ='999999';
 // var id = 'username';
 //     usernamerem = 'username';
 var myregexp = /^[a-zA-Z0-9]+$/;
 console.log(data.room); 
	if (myregexp.test(data.room) === true)   {
socket.leave('public');
 socket.join(data.room);
 socket.nickname = data.usernamerem;
 // console.log(io.of('/').adapter.rooms);
  // console.log (Object.keys(io.of('/').adapter.room));
//   var numClients = io.sockets.clients(room).length;
 // var roster = io.sockets.clients(data.room);
// var usersInRoom = io.sockets.clients(room);

// To:
// var roster = io.of('/chat').in('999999').clients;
// (function(callback){ console.log(callback.toString()); })(roster);
var listautenti = '';
// Array.from(roster.children).forEach(function(client) {
// listautenti =	listautenti +  client.nickname + '<br />';
// }); 
listautenti = 'LIST USERS IN THIS ROOM: ' +  listautenti;
 
 
socket.emit('setuproomser', {
			'room' :  data.room,
				'inforoom' : 'YOUR ROOM NAME IS VALID,<br />NOW YOUR PRIVATE ROOM IS ' + data.room + '<br />',
				'listautenti' : listautenti
			});
socket.broadcast.to(data.room).emit('suonacampser', data);
socket.broadcast.to(data.room).emit('listautentiser', {
							'listautenti' : listautenti		
									});

}  else {
//		socket.join('public');	
socket.nickname = data.usernamerem;		
 console.log('ERRORE STANZA');
// console.log (Object.keys(io.sockets.manager.rooms));
	socket.emit('setuproomserKO', {
				'room' : 'public',
				'inforoom' : 'YOUR ROOM NAME IS NOT VALID,   REMEMBER TO USE AT LEAST THREE CHARACTERS OF TYPE ONLY LETTERS AND/OR NUMBERS, NOTHING ELSE.  NOW YOUR ROOM IS PUBLIC'
			}); 
 var roster = io.sockets.clients('public');
var listautenti = '';
roster.forEach(function(client) {
listautenti =	listautenti +  client.nickname + '<br />';
}); 
console.log (listautenti);
listautenti = 'LIST USERS IN THIS ROOM: ' +  listautenti;	
socket.broadcast.to(data.room).emit('listautentiser', {
							'listautenti' : listautenti		
									});
}
	});

	// Start listening for mouse move events
	socket.on('mousemove', function (data) {
			
		socket.broadcast.to(data.room).emit('moving', data);
	     
	});
	
		
socket.on('salvasulserver', function (data) {
		
	//	var object = { foo: data.dataserver };
	var datidalclient = data.dataserver.replace(/^data:image\/\w+;base64,/, "");
var buf = new Buffer(datidalclient, 'base64');
//var string = 'scrivo qualche cosa';
var req = client.put(data.orario + '.png', {
    'Content-Length': buf.length,
	'Content-Type': 'image/png'
});
req.on('response', function(res){
  if (200 == res.statusCode) {
    console.log('saved to %s', req.url);    
  }
});
req.end(buf);		
});	

socket.on('doppioclick', function (data) {
		
		// This line sends the event (broadcasts it)
		// to everyone except the originating client.
	socket.broadcast.to(data.room).emit('doppioclickser', data);
				      
	});	

socket.on('chat', function (data) {
		
		// This line sends the event (broadcasts it)
		// to everyone except the originating client.
		socket.broadcast.to(data.room).emit('chatser', data);
	});	
socket.on('fileperaltri', function (data) {
		
		// This line sends the event (broadcasts it)
		// to everyone except the originating client.
	socket.broadcast.to(data.room).emit('fileperaltriser', data);
	});	

socket.on('rubber', function (data) {
socket.broadcast.to(data.room).emit('rubberser', data);
	});	

socket.on('camperaltri', function (data) {
	 socket.broadcast.to(data.room).emit('camperaltriser', data);		
	});	
	
});
io.on('connection', function(socket) {
   console.log('A user helo connected');
   

   //Whenever someone disconnects this piece of code executed
   socket.on('disconnect', function () {
      console.log('A user disconnected');
   });
   
});
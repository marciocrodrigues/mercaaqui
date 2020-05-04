const express = require('express')
const nunjucks = require('nunjucks')
const routes = require('./routes')

const server = express()

const port = process.env.PORT || 3000

server.use(express.urlencoded({ extended: true }))
server.use(express.static('public'))
server.use(routes)

server.set('view engine', 'njk')

nunjucks.configure('src/app/pages', {
    express: server,
    noCache: true,
    autoescape: false
})

server.listen(port, () => {
    console.log(' - Server Online - ')
})
const express = require('express')
const { LocalStorage } = require('node-localstorage')

const routes = express.Router()

const apiDeliverer = require('./services/deliverer')
const apiSessao = require('./services/sessao')
const apiSeller = require('./services/seller')
const apiProduct = require('./services/product')

const localStorage = new LocalStorage('./scratch')

/* HOME */
routes.get('/', (req, res) => {

    return res.render('home/index')
})

/* SELLERS */
routes.get('/sellers', (req, res) => {
    let stores = []

    for (i=1; i<=8; i++) {
        stores.push({
            photo: 'https://via.placeholder.com/500x500',
            name: `Loja ${i}`,
            description: 'Vendemos máscaras e produtos de limpeza'
        })
    }

    return res.render('home/sellers', { stores })
})

/* DELIVERERS */
routes.get('/deliverers', async (req, res) => {
    let deliverers = await apiDeliverer.index();

    return res.render('home/deliverers', { deliverers })
})

/* USERS */
routes.get('/users/register', (req, res) => {
    
    return res.render('user/register')
})

routes.get('/users/login', (req, res) => {
    return res.render('session/login')
})

routes.post('/users/login', async (req, res) => {
    const { email, password } = req.body;

    const dados = {
        email: email,
        senha: password
    }

    const result = await apiSessao.GerarToken(dados)

    localStorage.setItem('tipoSessao', result.tipo)
    localStorage.setItem('tokenSessao', result.token)

    if(result.tipo === 'CO'){

        const seller = await apiSeller.index(result.iD_Identificador)
        
        const products = await apiProduct.index(result.iD_Identificador)

        return res.render('profile/index', { products, seller })

    }
    
})

/* PROFILE */
routes.get('/profile/seller', (req, res) => {
    let products = []

    for (i=1; i<=4; i++) {
        products.push({
            photo: 'https://via.placeholder.com/500x500',
            name: `Produto ${i}`,
            price: 'R$4,00'
        })
    }

    return res.render('profile/index', { products })
})

module.exports = routes
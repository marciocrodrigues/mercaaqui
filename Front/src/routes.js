const express = require('express')
const fs = require('fs');
const { LocalStorage } = require('node-localstorage')

const routes = express.Router()

const apiDeliverer = require('./services/deliverer')
const apiSessao = require('./services/sessao')
const apiSeller = require('./services/seller')
const apiProduct = require('./services/product')

const encoded64 = require('./utils/base64')

const localStorage = new LocalStorage('./scratch')

/* HOME */
routes.get('/', (req, res) => {

    return res.render('home/index')
})

/* SELLERS */
routes.get('/sellers', (req, res) => {

    const { estado, cidade } = req.query

    const data = {
        estado: estado,
        cidade: cidade
    }

    const response = await apiSeller.searchSeller(data)

    console.log(response)

    //return res.render('home/sellers', { stores })
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

routes.get('/users/product', (req, res) => {
    return res.render('product/index')
});

routes.post('/users/product', async (req, res) => {
    const { descricao, quantidade, preco, imagem } = req.body

    let file = ''

    if(imagem !== ''){
        const imagemBase64 = fs.readFileSync(imagem, { encoding: 'base64' })

        file = `data:image/png;base64,${imagemBase64}`   
    }

    const idComerciante = localStorage.getItem('idComerciante')

    const data = {
        iD_Produto: 0,
        iD_Comerciante: parseInt(idComerciante),
        descricao: descricao,
        quantidade: parseInt(quantidade),
        preco: parseFloat(preco),
        imagem: file
    }

    const response = await apiProduct.register(data)

    if(response.Returno_Code !== 0){

        const iD_Identificador = localStorage.getItem('idComerciante')

        const seller = await apiSeller.index(iD_Identificador)
        
        const products = await apiProduct.index(iD_Identificador)

        return res.render('profile/index', { products, seller })
    }

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

        localStorage.setItem('idComerciante',result.iD_Identificador)

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
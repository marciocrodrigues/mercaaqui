const axios = require('axios');
const { LocalStorage } = require('node-localstorage')

const urlBase = require('../../urlApi')

const localStorage = new LocalStorage('./scratch')

class Product {
  
  async register(dados){
    return await axios.post(`${urlBase}Cadastrar/cadastrar-produto`, dados, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('tokenSessao')}`,
      }})
      .then((response) => {
        return response.data
      })
      .catch(e => console.error(e))
  }

  async index(id){
     return await axios.get(`${urlBase}Comerciante/produtos/${id}`,{
      headers: {
          Authorization: `Bearer ${localStorage.getItem('tokenSessao')}`,
      }})
      .then((response) => {
        return response.data
      })
      .catch(e => console.log(e))
  }
}

module.exports = new Product();
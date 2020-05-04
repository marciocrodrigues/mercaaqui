const axios = require('axios');
const { LocalStorage } = require('node-localstorage')

const urlBase = require('../../urlApi')

const localStorage = new LocalStorage('./scratch')

class Product {
  
  async register(dados){

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
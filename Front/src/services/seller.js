const axios = require('axios');
const { LocalStorage } = require('node-localstorage')

const urlBase = require('../../urlApi')

const localStorage = new LocalStorage('./scratch')

class Seller {
  
  async register(dados){

  }

  async searchSeller(dados){
    console.log(`${urlBase}Comerciante/comerciantes/cidade-estado`)
    return await axios.post(`${urlBase}Comerciante/comerciantes/cidade-estado`, dados, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('tokenSessao')}`,
      }})
    .then((response) => {
      return response.data
    })
    .catch(e => console.log(e))
  }

  async index(id){
     return await axios.get(`${urlBase}Comerciante/${id}`,{
      headers: {
          Authorization: `Bearer ${localStorage.getItem('tokenSessao')}`,
      }})
      .then((response) => {
        return response.data
      })
      .catch(e => console.log(e))
  }
}

module.exports = new Seller();
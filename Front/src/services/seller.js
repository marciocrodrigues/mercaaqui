const axios = require('axios');
const { LocalStorage } = require('node-localstorage')

const urlBase = require('../../urlApi')

const localStorage = new LocalStorage('./scratch')

class Seller {
  
  async register(dados){

  }

  async searchSeller(dados){
    return await axios.get(`${urlBase}Comerciante/comerciantes/cidade-estado?Cidade=${dados.cidade}&Estado=${dados.estado}`, {
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
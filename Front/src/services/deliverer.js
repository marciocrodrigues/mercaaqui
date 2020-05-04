const axios = require('axios');
const { LocalStorage } = require('node-localstorage')

const urlBase = require('../../urlApi')

const localStorage = new LocalStorage('./scratch')

class Deliverer {
  
  async register(dados){

  }

  async index(){
     return await axios.get(`${urlBase}Entregador/listar-entregadores`,{
      headers: {
          Authorization: `Bearer ${localStorage.getItem('tokenSessao')}`,
      }})
      .then((response) => {
        return response.data
      })
      .catch(e => console.log(e))
  }
}

module.exports = new Deliverer();
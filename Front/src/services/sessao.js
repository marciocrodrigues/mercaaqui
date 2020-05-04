const axios = require('axios');

const urlBase = require('../../urlApi');

class Sessao {

  async GerarToken(dados){

     return await axios.post(`${urlBase}Sessao/token`, dados)
      .then((response) => {
        return response.data
      })
      .catch(e => console.log(e));
  }
}

module.exports = new Sessao();
# language: pt
# encoding: utf-8

Funcionalidade: Gestão de produtos
    Como um sistema integrador
    Quero poder criar um produto
    Para poder realizar pedidos

Cenario: Criação de produto
    Dado que um informo dados válidos para cadastrar um produto
    E submeto o post para a rota products
    Então o produto é criado com sucesso
    E o produto deve constar no banco de dados
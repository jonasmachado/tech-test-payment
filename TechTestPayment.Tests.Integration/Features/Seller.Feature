# language: pt
# encoding: utf-8

Funcionalidade: Gestão de vendedores
    Como um sistema integrador
    Quero poder criar um vendedor
    Para poder realizar pedidos

Cenario: Criação de vendedor
    Dado que um informo dados válidos para cadastrar um vendedor
    E submeto o post para a rota sellers
    Então o vendedor é criado com sucesso
    E o vendedor deve constar no banco de dados

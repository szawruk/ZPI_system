import axios from "axios";
import router from "@/router";

export default {
    namespaced: true,
    state: {
        userLoggedIn: false,
        userTeamId: 0
    },
    mutations: {
        userLogin(state, payload) {
            state.userLoggedIn = payload
        },
        setUserTeamId(state, payload) {
            state.userTeamId = payload
        }
    },
    actions: {
        userLogin({commit}, data) {
            axios.post('/api/Account/login',
                {Email: data.emailText, Password: data.passwordText}, {
                    headers: {'Authorization': 'Bearer ' + localStorage.getItem('tokennn')}
                })
                .then(response => {
                    if (response.status === 200) {
                        localStorage.setItem('tokennn', response.data)
                        commit('userLogin', true)
                        router.push('/').then()
                    }
                })
        },
        userLogout({commit}, data) {
            const token = localStorage.getItem('tokennn')
            axios.get('/api/Account/logout',{
                headers:{
                    authorization: 'Bearer ' + token
                }
            })
                .then(response => {
                    if (response.status === 200) {
                        localStorage.removeItem('tokennn')
                        commit('userLogin', false)
                        router.push('/').then()
                    }
                })
        },
        userRegister({commit}, data) {
            axios.post('/api/Account/register',
                {
                    Name: data.nameText,
                    Surname: data.surnameText,
                    Email: data.emailText,
                    Password: data.passwordText,
                    AccountType: data.accountText,
                    Index: data.indexText,
                })
                .then(response => {
                    if (response.status === 201) {
                        router.push('/login').then()
                    }
                })
        }
    }
}
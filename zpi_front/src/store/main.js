import axios from "axios";
import router from "@/router";

const config = {
    headers: { Authorization: `Bearer ${token}` }
};


export default {
    namespaced: true,
    state: {
        userLoggedIn: false,
        userTeamId: 0
    },
    mutations: {
        userLogin(state, payload){
            state.userLoggedIn = payload
        },
        setUserTeamId(state, payload){
            state.userTeamId = payload
        }
    },
    actions: {
        userLogin({commit}, data) {
            axios.post('/api/Account/login',
                {Email: data.emailText, Password: data.passwordText}, {
                headers: { Authorization: `Bearer ${JSON.parse(sessionStorage.getItem('tokennn'))}` }
            })
                .then(response => {
                    console.log(response)
                    router.push('/').then()
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
                    console.log(response)
                    //TODO save token to localstorage or cookie
                    router.push('/').then()
                })
        }
    }
}
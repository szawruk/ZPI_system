import axios from "axios";
import router from "@/router";

export default {
    namespaced: true,
    state: {
        userLoggedIn: true,
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
            axios.post('/api/account/login', {Email: data.emailText, Password: data.passwordText})
                .then(response => {
                    console.log(response)
                    //TODO save token to localstorage or cookie
                    router.push('/').then()
                })
        }
    }
}
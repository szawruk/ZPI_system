export default {
    namespaced: true,
    state: {
        userLoggedIn: false
    },
    mutations: {
        userLogin(state, payload){
            state.userLoggedIn = payload
        }
    },
    actions: {
        userLogin({commit, state}){
            // TODO login axios
        }
    }
}
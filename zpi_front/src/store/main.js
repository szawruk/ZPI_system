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
        userLogin({commit, state}){
            // TODO login axios
        }
    }
}
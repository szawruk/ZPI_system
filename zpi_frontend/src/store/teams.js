export default {
    namespaced: true,
    state: {
        teamsList: [
            {
                name: 'nazwa1',
                topic: 'temat1',
                description: "opis1",
            },
            {
                name: 'nazwa2',
                topic: 'temat2',
                description: "opis2",
            },
            {
                name: 'nazwa3',
                topic: 'temat3',
                description: "opis3",
            },
            {
                name: 'nazwa4',
                topic: 'temat4',
                description: "opis4",
            }]
    },
    mutations: {
        setTeamsList(state, payload) {
            state.teamsList = payload
        }
    },
    actions: {
        getTeamsList({commit, state}) {
            // TODO axios
        }
    }
}
import axios from "axios";
import router from "@/router";

export default {
    namespaced: true,
    state: {
        selectedMenuOptions: 1,
        tasksList: []
    },
    mutations: {
        setSelectedMenuOptions(state, payload) {
            state.selectedMenuOptions = payload
        }
    },
    actions: {
        getTasks({commit, state}) {
            axios.get('/api/users')
                .then(response => {
                    if (response.status === 200) {
                        console.log(response.data)
                        commit('setTeamsList', response.data)
                    }
                })
        },
        saveTeam({commit}, data) {
            axios.post('/api/teams', {
                Team: {
                    Name: data.teamText,
                    Description: data.descriptionText
                },
                StudentId: 1,
                TopicId: data.topicId
            })
                .then(response => {
                    console.log(response)
                    router.push('/topics').then()
                })
        }
    }
}
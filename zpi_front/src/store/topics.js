import axios from "axios";
import router from "@/router";

export default {
    namespaced: true,
    state: {
        topicsList: [
            {
                id: 1,
                topic: 'temat1',
                description: "opis1",
            },
            {
                id: 2,
                topic: 'temat2',
                description: "opis2",
            },
            {
                id: 3,
                topic: 'temat3',
                description: "opis3",
            },
            {
                id: 4,
                topic: 'temat4',
                description: "opis4",
            }]
    },
    mutations: {
        setTopicsList(state, payload) {
            state.topicsList = payload
        }
    },
    actions: {
        getTopicsList({commit, state}) {
            // TODO axios
        },
        saveTopic({commit}, data) {
            axios.post('/api/topics', {name: data.topicText, description: data.descriptionText})
                .then(response => {
                    console.log(response)
                    router.push('/topics').then()
                })
        }
    }
}
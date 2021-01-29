import VueRouter from 'vue-router'
import MainPage from "@/components/MainPage.vue";
import Teams from "@/components/Teams";
import Topics from "@/components/Topics";
import CreateTopic from "@/components/CreateTopic";
import CreateTeam from "@/components/CreateTeam";
import MyTeam from "@/components/MyTeam";
import CreateTask from "@/components/CreateTask";
import Login from "@/components/Login";
import Register from "@/components/Register";
import Vue from 'vue'

Vue.use(VueRouter)

const routes = [
    {
        path: "/",
        name: "Home",
        component: MainPage,
    },
    {
        path: "/login",
        name: "Login",
        component: Login,
    },
    {
        path: "/register",
        name: "Register",
        component: Register,
    },
    {
        path: "/topics/new",
        name: "CreateTopic",
        component: CreateTopic,
    },
    {
        path: "/teams/new",
        name: "CreateTeam",
        component: CreateTeam,
    },
    {
        path: "/teams",
        name: "Teams",
        component: Teams,
    },
    {
        path: "/topics",
        name: "Topics",
        component: Topics,
    },
    {
        path: "/my-team",
        name: "MyTeam",
        component: MyTeam,
    },
    {
        path: "/my-team/tasks/new",
        name: "CreateTask",
        component: CreateTask,
    },


    
];

const router = new VueRouter({
    routes,
})

export default router;
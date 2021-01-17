import { createWebHistory, createRouter } from "vue-router";
import HelloWorld from "@/components/HelloWorld.vue";
import MainPage from "@/components/MainPage.vue";

const routes = [
    {
        path: "/",
        name: "Home",
        component: MainPage,
    },
    
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
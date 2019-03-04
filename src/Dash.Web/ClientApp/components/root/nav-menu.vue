<template>
   
    <ul
        class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion"
        id="accordionSidebar"
      >
        <!-- Sidebar - Brand -->
        <router-link class="sidebar-brand d-flex align-items-center justify-content-center" to="/">
            <div class="sidebar-brand-icon rotate-n-15">
                <i class="fas fa-laugh-wink"></i>
                <icon icon="assistive-listening-systems" size="2x" />
            </div>
            <div class="sidebar-brand-text mx-3">SB Admin
                <sup>2</sup>
            </div>
        </router-link>

        <!-- Divider -->
        <hr class="sidebar-divider my-0">

        <li class="nav-item" v-for="(route, index) in routes" :key="index">
            <router-link class="nav-link" :to="route.path" exact-active-class="active">
                <icon :icon="route.icon" class="mr-2" /><span>{{ route.display }}</span> 
            </router-link>
        </li>

        <!-- TODO: alt menülü navigasyon ihtiyacında burası düzenlenecek -->

        <!-- Divider -->
        <!-- <hr class="sidebar-divider"> -->

        <!-- Heading -->
        <!-- <div class="sidebar-heading">Interface</div> -->

        <!-- Nav Item - Pages Collapse Menu -->
        <!-- <li class="nav-item">
          <a
            class="nav-link collapsed"
            href="#"
            data-toggle="collapse"
            data-target="#collapseTwo"
            aria-expanded="true"
            aria-controls="collapseTwo"
          >
            <i class="fas fa-fw fa-cog"></i>
            <span>Components</span>
          </a>
          <div
            id="collapseTwo"
            class="collapse"
            aria-labelledby="headingTwo"
            data-parent="#accordionSidebar"
          >
            <div class="bg-white py-2 collapse-inner rounded">
              <h6 class="collapse-header">Custom Components:</h6>
              <a class="collapse-item" href="buttons.html">Buttons</a>
              <a class="collapse-item" href="cards.html">Cards</a>
            </div>
          </div>
        </li> -->

        <!-- Divider -->
        <hr class="sidebar-divider d-none d-md-block">

        <!-- Sidebar Toggler (Sidebar) -->
        <div class="text-center d-none d-md-inline">
          <button class="rounded-circle border-0" id="sidebarToggle"> <icon icon="angle-left" /> </button>
        </div>
      </ul>
      <!-- End of Sidebar -->




</template>

<script>
    import { routes } from "../../router/routes";

    export default {
      data () {
        return {
          routes,
          collapsed: true
        }
      },
      methods: {
      }
    }
</script>

<style scoped>
</style>
# DPVR Unity SDK Instructions

> These instructions are relevant to DPVR SDK version 0.7.4

Instructions for how to create new Unity virtual reality experiences for the DPVR virtual reality and personal cinema headsets (or port existing ones).

These guides target the DPVR P1 (Professional One) Headset for mobile-vr applications, and it's compatible Flip controller.

<p align="center">
  <img alt="P1 Headset" width="500px" src="/docs/assets/DPVR P1.svg">
</p>

## DPVR All-In-One

#### Support & Revenue share

WEARVR.com, The world's largest independent VR app store, has partnered with Shanghai Lexiang Technology Co., Ltd to provide development kits and assistance with promotion, technical support and advice to help get your content into DPVR's marketplace - at no cost to you. You get the same high revenue share of 80%.

| Region | Developer Revenue Share |
| :---: | :----: |
| Worldwide | 80% |

#### Specifications

View the [full headset specifications](https://www.wearvr.com/developer-center/devices/dpvr).

#### Requesting a development kit

You can [request a DPVR All-In-One](/docs/dpvr-development-kit.md) device to help get your VR experiences DPVR-compatible.

## Prerequisites

You will require the following in order to develop a DPVR app:

A compatible version of Unity:

* Unity3D 5.3.4 
* Unity3D 5.4.3 
* Unity3D 5.5.0 
* Unity3D 5.6.1 
* Unity3D 2017.* 
* Unity3D 2018.* 

> All Unity3D 2017 and 2018 versions should be compatible with the DPVR SDK, but only some older versions of Unity are verified to work correctly. Developers should try to match their projects with these versions to reduce potential risks.

## Overview

You can easily create a new Unity VR app, but the fastest way to get up and running on a DPVR device is to convert an existing Google Cardboard, Google Daydream, Pico or Samsung Gear VR experience.

* [Installing and configuring the DPVR SDK](/docs/dpvr-vr-unity-sdk-installation.md)
* [Controller and headset inputs](/docs/dpvr-all-in-one-controllers.md)
* [Configuring the AndroidManifest file](/docs/android-manifest.md)
* [Building to the device](/docs/building-to-dpvr-all-in-one.md)

Optional:

* [DPVR Example Scenes](/docs/dpvr-example-scenes-overview.md)
* [Porting considerations](/docs/dpvr-porting-considerations.md)
* [Troubleshooting](/docs/troubleshooting.md)
* [Optimizing DPVR experiences](/docs/optimizing-dpvr-experiences.md)
* [Getting device information at runtime](/docs/getting-device-information-at-runtime.md)
* [Overview of API](/docs/api-overview.md)

Device:

* [Understanding the device interface](/docs/device-user-interface-guide.md)

There is an [example project](examples/Readme.md) to use as a reference as you follow this guide.

## Unable to port yourself?

Depending on availability and need, WEARVR may be able to provide additional support to help port your existing VR experiences to be compatible with the DPVR All-In-One System, all the way up to completing the port for you and localizing it for the Chinese market.

Please get in touch on `devs@wearvr.com` to discuss your needs and how WEARVR may be able to help.

## Copyright & Trademarks

These instructions and example project are maintained by WEARVR LLC, The largest independent virtual reality app store. WEARVR is interested in connecting VR content creators and consumers, globally. We love working with the VR community and would be delighted to hear from you at `devs@wearvr.com`.

You can find more information about WEARVR at www.wearvr.com

The DPVR trademark, Shanghai Lexiang Technology Co., Ltd Trademark, all DPVR virtual reality headsets and DPVR Unity SDK are all owned by [Shanghai Lexiang Technology Co., Ltd](http://dpvr.net/).


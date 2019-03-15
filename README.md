# DPVR Unity SDK Instructions

> These instructions are relevant to SDK version 0.7.4

Instructions for how to create new Unity virtual reality experiences for the DPVR virtual reality and personal cinema headsets (or port existing ones).

## DPVR All-In-One

#### Support & Revenue share

WEARVR.com, the world's largest independent VR app store, has partnered with Shanghai Lexiang Technology Co., Ltd to provide development kits and assistance with promotion, technical support and advice to help get your content into DPVR's marketplace - at no cost to you. You get the same high revenue share of 70%.

| Region | Developer Revenue Share |
| :---: | :----: |
| Worldwide | 70% |

> TODO: Confirm this percentage with team

#### Specifications

View the [full headset specifications](https://www.wearvr.com/developer-center/devices/dpvr).

> TODO: Confirm this page with team

#### Requesting a development kit

You can [request a DPVR All-In-One](/docs/dpvr-development-kit.md) device to help get your VR experiences DPVR-compatible.

> TODO: Confirm this page with team

## Prerequisites

You will require the following in order to develop a DPVR app:

A compatible version of Unity:

* Unity3D

### User accounts and adding in-app purchases

To access user information on the Pico Goblin headset, or add in-app purchases to your VR content, your app will normally need to already be [registered through WEARVR](https://users.wearvr.com/apps) to generate the necessary access credentials.

If this is a problem, you can get in touch via `devs@wearvr.com` to discuss your needs.

## Overview

You can easily create a new Unity VR app, but the fastest way to get up and running on Pico Goblin is to convert an existing Google Cardboard, Google Daydream or Samsung Gear VR experience.

* [Installing and configuring the Pico VR Unity SDK](/docs/pico-vr-unity-sdk-installation.md)
* [Camera & input module setup](/docs/pico-vr-camera-setup.md)
* [Controller and headset inputs](/docs/pico-goblin-and-neo-controllers.md)
* [Enabling USB debugging](/docs/pico-goblin-developer-mode-usb-debugging.md)
* [Building to the device](/docs/building-to-pico-goblin.md)

Optional:

* [Working with the current user](/docs/pico-payment-sdk-user-management.md)
* [Adding in-app purchases](/docs/pico-payment-sdk-in-app-purchases.md)
* [Testing in-app purchases](/docs/testing-in-app-purchases.md)
* [Troubleshooting](/docs/troubleshooting.md)
* [Optimizing Pico experiences](/docs/optimizing-pico-experiences.md)
* [Localization](/docs/pico-unity-localization.md)
* [Getting device information at runtime](/docs/getting-device-information-at-runtime.md)
* [Overview of API](/docs/api-overview.md)

Device:

* [Connecting to a Wifi network](/docs/connecting-to-a-wifi-network.md)
* [Upgrading the Pico Goblin OS & Firmware](/docs/upgrading-pico-goblin-operating-system-firmware.md)
* [Changing the Pico Goblin's language](/docs/changing-pico-goblins-language-setting.md)

There is an [example project](examples/PicoUnityVRSDKExample/Readme.md) to use as a reference as you follow this guide.

## Unable to port yourself?

Depending on availability and need, WEARVR may be able to provide addition support to help port your existing VR experiences to be compatible with the DPVR All-In-One System, all the way up to completing the port for you and localizing it for the Chinese market.

Please get in touch on `devs@wearvr.com` to discuss your needs and how WEARVR may be able to help.

## Copyright & Trademarks

These instructions and example project are maintained by WEARVR LLC, the largest independent virtual reality app store. WEARVR is interested in connecting VR content creators and consumers, globally. We love working with the VR community and would be delighted to hear from you at `devs@wearvr.com`.

You can find more information about WEARVR at www.wearvr.com

The DPVR trademark, Shanghai Lexiang Technology Co., Ltd Trademark, all DPVR virtual reality headsets and DPVR Unity SDK are all owned by [Shanghai Lexiang Technology Co., Ltd](http://dpvr.net/).

